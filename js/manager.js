/* javascript document*/
if (document.getElementById('editFlag').value != 'false') {
    getShowEdit = document.getElementById('AddMenberBtn');
    getShowEdit.onclick = function () {
        document.getElementById('edit').style.display = "block";
    }
}
function cancleCreate() {
    document.getElementById('edit').style.display = "none";
}
 function showEditPerson () {
     document.getElementById('editPerson').style.display = "block";
}
function cancleChange() {
    document.getElementById('editPerson').style.display = "none";
}
if (document.getElementById('GradeIn') != undefined) {
    document.getElementById('GradeIn').value = ( (new Date).getFullYear() - 1).toString().substring(2, 4);
}
x=document.getElementById('member').getElementsByTagName('ul');
for (j=0; j<x.length;j++ )
{	x[j].style.display="none";
}
checkBoxs = document.getElementById('information').getElementsByTagName('input');
var cmen=document.getElementById('changeMenHid');
cmen.value="";
if (document.getElementById('editFlag').value == 'true') {
    for (i = 0; i < checkBoxs.length; i++) {
        e = checkBoxs[i];
        if (e.type == 'checkbox') {
            e.style.display = "block"; 
            if (cmen.value == "") cmen.value = checkBoxs[i].id;
            else cmen.value += (',' + checkBoxs[i].id);
        }
        else if (e.type == 'text' && e.id.substr(0,20)!='userListRpt_realName') { e.removeAttribute('readOnly'); }
    }
}
function CheckNull(element) {
    var warning = document.getElementById(element.id + 'Span');
    if (element.value == "") {
        warning.style.visibility = 'visible';
        return false;
    }
    else {
        warning.style.visibility = 'hidden';
        return true;
    }
}
function CheckRePw(e1, e2) {
    if (e1.value != e2.value) document.getElementById('PwCompareSpan').style.visibility = 'visible';
   return CheckNull(e1)&&(e1.value == e2.value ? true : false);
}
function CheckChangAll() {
    var pw = document.getElementById('newPwIn');
    var rePw = document.getElementById('reNewPwIn')
    if (CheckNull(document.getElementById('loginNameIn')) & CheckNull(document.getElementById('oldPwIn')) & CheckNull(pw) & CheckRePw(rePw,pw) & isDigit(document.getElementById('rLongNumIn').value,document.getElementById('rNumSpan')) & isDigit(document.getElementById('rShortNumIn').value,document.getElementById('rNumSpan'))!=0)
     return true;
    return false;    
}
function isDigit(s,numSpan) {
    var patrn = /^[0-9]{1,20}$/;
    if (!patrn.exec(s) && s.length != 0) { numSpan.style.visibility = 'visible'; return false; }
    else return true;
}

function showUl(show,i){
    y = show.getElementsByTagName('span')[0];
    if (y != undefined) {
        if (x[i].style.display == "none") { x[i].style.display = "block"; y.style.backgroundPosition = "3px 6px"; }
        else { x[i].style.display = "none"; y.style.backgroundPosition = "3px -18px"; }
    }
    else {
        if (x[i].style.display == "none") { x[i].style.display = "block";}
        else { x[i].style.display = "none"; }
    }
}
function DeleteBtn_OnClick(){
    var menber = document.getElementById('menberHid');
   menber.value = "";
   for (i = 0; i < checkBoxs.length; i += 8) {
       if (checkBoxs[i].checked) {
           if (menber.value == "") menber.value =checkBoxs[i].id;
           else menber.value += (',' + checkBoxs[i].id);
       }
   }
}