function setCookie(cookieName, cookieValue, expirationDays) {
    var date = new Date();
    date.setTime(date.getTime() + (expirationDays * 24 * 60 * 60 * 1000)); // Convert days to milliseconds
    var expires = "; expires=" + date.toUTCString();

    document.cookie = cookieName + "=" + cookieValue + expires + "; path=/";
}
function calculateSum(array, property) {
    const total = array.reduce((accumulator, object) => {
        return accumulator + object[property];
    }, 0);

    return total;
}