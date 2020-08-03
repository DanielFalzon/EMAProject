// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

import HcpPartialTable from './emitters/HcpPartialTable.js';

window.onload = function () {
    var hcp = new HcpPartialTable();
    hcp.init();
}