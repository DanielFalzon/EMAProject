import FetchApiCaller from '../helpers/FetchApiCaller.js';
import observableInstance from '../helpers/Observable.js';

export default class HcpPartialTableLoader {
    constructor() {
        this.selectedHcps = [];
    }

    init() {
        if (!document.querySelector('#hcptable_data')) return;
        this._fetchApiCaller = new FetchApiCaller();

        this._tableContainer = document.querySelector('#hcptable_data tbody');
        this._prevBtn = document.querySelector("#hcptable_prev");
        this._nextBtn = document.querySelector("#hcptable_next");

        observableInstance.subscribe("ToggleSelectedHcp", this.toggleSelectedStatus.bind(this));
        observableInstance.subscribe("PaginateResults", this.paginateResults.bind(this));
    }

    get apiCaller() {
        return this._fetchApiCaller;
    }

    get tableContianer() {
        return this._tableContainer;
    }

    get prevBtn() {
        return this._prevBtn;
    }

    get nextBtn() {
        return this._nextBtn;
    }

    async paginateResults(page) {
        let data = await this.apiCaller.callGet(`/JsonProvider/GetHcpPaginatedTable?pageNum=${page}`);
        let jsonData = JSON.parse(data);

        this.refreshResults(jsonData);
    }

    async toggleSelectedStatus(el) {
        /* D.F.
         * Will be rendered redundant as they need to be added to the session to be used on POST.
         */
        let params = {
            "hcpId": el.value
        }

        console.log(JSON.stringify(params));

        let data = await this.apiCaller.callPost(`/JsonProvider/ToggleSelectedHcp`, params);
        let jsonData = JSON.parse(data);

        el.checked = jsonData;

        console.log(jsonData);
    }

    refreshResults(jsonResults) {

        this.prevBtn.disabled = !jsonResults["ShowPrevious"];
        this.nextBtn.disabled = !jsonResults["ShowNext"];

        this.prevBtn.dataset.topage = jsonResults["CurrentPage"] - 1;
        this.nextBtn.dataset.topage = jsonResults["CurrentPage"] + 1;

        this.tableContianer.innerHTML = "";

        jsonResults["Data"].map((rowData) => {
             this.tableContianer.appendChild(this.buildRow(rowData));
        });

        console.log(jsonResults);
    }

    buildRow(rowData) {
        var newRow = document.createElement("tr");
        var checkBoxRow = document.createElement("td");

        var checkBox = document.createElement("input");

        checkBox.type = 'checkbox';
        checkBox.value = rowData["HealthCareProviderID"];
        checkBox.checked = rowData["isSelected"];
        checkBox.classList = 'js_hcptable-selectors';
        checkBoxRow.appendChild(checkBox);

        newRow.innerHTML = `<td>${rowData["Name"]}</td>` + `<td>${rowData["ContactNumber"]}</td>`;
        newRow.appendChild(checkBoxRow);

        return newRow;
    }
}
