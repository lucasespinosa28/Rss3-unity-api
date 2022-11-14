mergeInto(LibraryManager.library, {
    Connect: async function () {
        const accounts = await ethereum.request({ method: 'eth_requestAccounts' });
        const account = accounts[0];
        localStorage.setItem('metamaskAddress', account);
    },
    GetAddress: function(){
        if(localStorage["metamaskAddress"] != undefined){
            var returnStr = localStorage["metamaskAddress"]
            var bufferSize = lengthBytesUTF8(returnStr) + 1;
            var buffer =  _malloc(bufferSize);
            stringToUTF8(returnStr, buffer, bufferSize);
            return buffer
        }
        var returnStr = "empty"
        var bufferSize = lengthBytesUTF8(returnStr) + 1;
        var buffer =  _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize);
        return buffer
    }
})