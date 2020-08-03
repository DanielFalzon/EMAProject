import observableInstance from '../helpers/Observable.js';
import HcpPartialTableLoader from '../modules/HcpPartialTableLoader.js';

export default class HcpPartialTable {
    constructor() {

    }

    init() {
        if (!document.querySelector('#hcptable_data')) return;
        this.loader = new HcpPartialTableLoader();
        this.loader.init();

        this._pagBtns = document.querySelectorAll(".js_hcptable-pagination");
        //var _refButton = document.querySelector(".js_hcptable-refresh");
        this._itemCheckBoxes = document.querySelectorAll(".js_hcptable-selectors");

        this.assignClickEvents();
    }

    get paginationBtns() {
        return this._pagBtns;
    }

    assignClickEvents() {
        this.selectRow();
        this.paginateTable();
    }

    selectRow() {

    }

    paginateTable() {
        Array.from(this.paginationBtns).map((el) => {
            console.log(el);
        })
       observableInstance.publish("PaginateResults", 1);
    }
}