import FetchApiCaller from '../helpers/FetchApiCaller.js';
import observableInstance from '../helpers/Observable.js';

export default class HcpPartialTableLoader {
    constructor() {
        this.selectedHcps = [];
    }

    init() {
        if (!document.querySelector('#hcptable_data')) return;

        this._tableContainer = document.querySelector('#hcptable_data');
        this._fetchApiCaller = new FetchApiCaller();

        observableInstance.subscribe("ToggleSelectedHcp", this.toggleSelectedStatus.bind(this));
        observableInstance.subscribe("PaginateResults", this.paginateResults.bind(this));
    }

    get apiCaller() {
        return this._fetchApiCaller;
    }

    get tableContianer() {
        return this._tableContainer;
    }

    async paginateResults(page) {
        let data = await this.apiCaller.callGet(`/JsonProvider/GetHcpPaginatedTable?pageNum=${page}`);
        let jsonData = JSON.parse(data);

        this.refreshResults(jsonData);
    }

    toggleSelectedStatus(hcpID) {
        /* D.F.
         * Will be rendered redundant as they need to be added to the session to be used on POST.
         */
        if (this.selectedHcps.includes(hcpID) == true) {
            this.selectedHcps = this.selectedHcps.filter(item => item !== hcpID);
        } else {
            this.selectedHcps.push(hcpID);
        }
        console.log(this.selectedHcps);
    }

    refreshResults(jsonResults) {
        /* D.F
         * Method used to render the rows based on the returned data. 
         */
        console.log(jsonResults);
    }
}
