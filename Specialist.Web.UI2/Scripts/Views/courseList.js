var pageCount;

function initPager(pPageCount) {
    if (pageCount != null)
        return;
    pageCount = pPageCount;
    createPager(0);
}

function createPager(index) {
    if (index == null)
        index = 0  ;
    if (pageCount == 1) {
        $(".course-list-pager").html("&nbsp;");
        return;
    }
    var pager = "Страницы:";
    for (i = 0; i < pageCount; i++) {
        humanIndex = i + 1;
        var link = "<span>" + humanIndex + "</span>";
        if (i != index) {
            link = '<a href="#" class="course-page-link">' + humanIndex + '</a>';
        }
        pager += link;
    }
    $(".course-list-pager").html(pager);

    pagerCreated = true;
}
