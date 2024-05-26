﻿function collapsibleSections() {
    var classStr = document.getElementById("ContentPlaceHolder1_secDiv").getAttribute("class")

    if (classStr.includes("collapse")) {
        document.getElementById("ContentPlaceHolder1_secDiv").setAttribute("class", "navbar flex-column flex-grow w-100 bg-info")
    }
    else {
        document.getElementById("ContentPlaceHolder1_secDiv").setAttribute("class", "navbar collapse flex-column flex-grow w-100 bg-info")
    }
}