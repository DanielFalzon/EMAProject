import observableInstance from '../helpers/Observable.js';
import HcpPartialTableLoader from '../modules/HcpPartialTableLoader.js';

class HcpPartialTable {
    constructor() {

    }

    init() {
        if (!document.querySelector('#hcptable_data')) return;

        this.loader = new HcpPartialTableLoader();
        this.loader.init();

        this._table = document.querySelector('#hcptable_data');
        this._pagBtns = document.querySelectorAll(".js_hcptable-pagination");

        this.assignClickEvents();
    }

    get table() {
        return this._table;
    }

    get paginationBtns() {
        return this._pagBtns;
    }

    assignClickEvents() {
        this.selectRow();
        this.paginateTable();
    }

    selectRow() {
        this.table.addEventListener("click", function (e) {
            if (e.target.tagName === "INPUT") {
                observableInstance.publish("ToggleSelectedHcp", e.target);
            }
        })
    }

    paginateTable() {
        Array.from(this.paginationBtns).map((el) => {
            el.addEventListener("click", function () {
                observableInstance.publish("PaginateResults", el.dataset.topage);
            });
        })
    }
}

const hcpPartialTable = new HcpPartialTable();
export default hcpPartialTable;