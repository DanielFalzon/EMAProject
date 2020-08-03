import FetchApiCaller from '../helpers/FetchApiCaller.js';
import observableInstance from '../helpers/Observable.js';

export default class HcpPartialTableLoader {
    constructor() {
       
    }

    init() {
        if (!document.querySelector('#hcptable_data')) return;

        this._tableContainer = document.querySelector('#hcptable_data');
        this._fetchApiCaller = new FetchApiCaller();
        console.log(this._fetchApiCaller);
        observableInstance.subscribe("PaginateResults", this.paginateResults.bind(this));
    }

    get apiCaller() {
        return this._fetchApiCaller;
    }

    get tableContianer() {
        return this._tableContainer;
    }

    async paginateResults(page) {
        console.log(`/JsonProvider/GetHcpPaginatedTable?pageNum=${page}`);
        var data = await this.apiCaller.callGet(`/JsonProvider/GetHcpPaginatedTable/${page}`);
        this.refreshResults(JSON.parse(data));
    }

    refreshResults(jsonResults) {
        console.log(jsonResults);
    }
}
