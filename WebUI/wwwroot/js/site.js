function convertFirstLetterToUpperCase(text) {
    return text.charAt(0).toUpperCase() + text.slice(1);
}

function convertShortDate(date) {
    var shortDate = new Date(date).toLocaleDateString("tr-TR");
    return shortDate;
}