var steps = document.getElementById('stepDiv').childNodes;
for (var i = 1; i < steps.length; i++) {
    if (i % 2 != 0) {
        var element = steps[i];
        element.size = element.value.replace(/[^\u0000-\u00ff]/g, "aa").length;
    }
}
function SetHidden() {
    var steps = document.getElementById('stepDiv').childNodes;
    document.getElementById('stepHiddens').value = "";
    document.getElementById('groupProLenHid').value = document.getElementById('groupProgress').style.width;
    for (var i = 1; i < steps.length; i++) {
        if (i % 2 != 0) {
            var element = steps[i];
            if (element.value != "") {
                if (document.getElementById('stepHiddens').value == "") document.getElementById('stepHiddens').value = element.value;
                else document.getElementById('stepHiddens').value += (',' + element.value);
            }
        }
    }
}
function DeleteMess() {
    editTextarea = document.getElementById('messageTextarea');
    editTextarea.value = "";
}
function SetAbled() {
    var editTextArea = document.getElementById('messageTextarea');
    editTextArea.disabled = false;
    editTextArea.focus();
}
function SetInputWidth(which) {
    which.size = which.value.replace(/[^\u0000-\u00ff]/g, "aa").length;
}
function SetGroupProgressLength(which) {
    var steps = document.getElementById('stepDiv').childNodes;
    for (var i = 1, count = 0; i < steps.length; i++) {
        if (i % 2 != 0) {
            var element = steps[i];
            count++;
            if (element == which) {
                document.getElementById('groupProgress').style.width = (count / ((steps.length - 1) / 2 + 1)) * 554 + "px";
            }
        }
    }
}
function AddLength(which) {
    iCount = which.value.replace(/[^\u0000-\u00ff]/g, "aa");
    which.size = iCount.length + 2;
}
function SetGroupFinish() {
    document.getElementById('groupProgress').style.width = 554 + "px";
}
function CheckFile() {
    var fileFullName = document.getElementById('InputFile').value;
    if (fileFullName == null || fileFullName == "") {
         alert("请选择文件!");return false;
     }
     var index = fileFullName.lastIndexOf(".");
    var extendName = fileFullName.substr(index + 1, fileFullName.length - index - 1);
    if (!(extendName == "rar" || extendName == "zip" || extendName == "gz" || extendName == "7z" || extendName == "7Z" || extendName == "RAR" || extendName == "ZIP" || extendName == "GZ")) {
    alert("老兄，只能上传压缩文件!"+extendName); 
    return false; 
    }
        else return true;
    }
    function CheckGrossChar(e, eFor, origiText, num) {
        getInner = e.value;
        if (getInner.length > num) {
            overtext = getInner.length - num;
            eFor.innerHTML = '(字数超过' + overtext + '字请修改）';
            eFor.style.color = 'red';
            e.focus();
            return false;
        }
        else {
            eFor.innerHTML = origiText;
            eFor.style.color = '#808080';
            return true;
        }
    }
    function CheckNull(e1, warning) {
        if (e1.value.length == 0) { warning.style.visibility = 'visible'; return false; }
        else { warning.style.visibility = 'hidden'; return true; }
    }
    function isDigit(e, numSpan, maxNum) {
        var patrn = /^[0-9]{1,20}$/;
        if (!patrn.exec(e.value) && e.value.length != 0) { numSpan.innerText = '只能是数字'; numSpan.style.visibility = 'visible'; return false; }
        else if (maxNum != 0 && parseInt(e.value) > maxNum) { numSpan.innerText = '不能大于' + maxNum.toString(); numSpan.style.visibility = 'visible'; return false; }
        else return true;
    }