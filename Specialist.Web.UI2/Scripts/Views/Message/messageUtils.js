function setupMce(id) {
    tinyMCE.init({
    relative_urls: false,
    language: 'ru',
    mode: 'exact',
    elements: id,
    gecko_spellcheck : true,
    theme: 'advanced',
    theme_advanced_toolbar_location: 'top',
    theme_advanced_toolbar_align: 'left',
    plugins:"paste",
    theme_advanced_resizing: true,
    theme_advanced_buttons1:'bold,italic,strikethrough,sub,sup,charmap,|,justifyleft,justifycenter,justifyright,justifyfull,|,formatselect,fontsizeselect',
    theme_advanced_buttons2:'paste,pastetext,pasteword,|,bullist,numlist,|,outdent,indent,|,undo,redo,|,link,unlink,image,emotions,cleanup,code,|,forecolor,backcolor',
    theme_advanced_buttons3:''

    });
}