/* javascript document*/
getShowEdit = document.getElementById('showEdit');
getShowEdit.onclick = function () {
    document.getElementById('edit').style.display = "block";
    if (getShowEdit.innerHTML == "创建新的项目")
        document.getElementById('create').style.display = "block";
    else if (getShowEdit.innerHTML == "添加")
        document.getElementById('create_financial').style.display = "block";
    else if ((getShowEdit.innerHTML == "发布"))
        document.getElementById('create_notice').style.display = "block";
}
function ShowCreate_notice() {
    document.getElementById('create_notice').style.display = "block";
    document.getElementById('edit').style.display = "block";
}
function cancleCreate() {
    document.getElementById('edit').style.display = "none";
    document.getElementById('create').style.display = "none";
    document.getElementById('create_financial').style.display = "none";
    document.getElementById('create_notice').style.display = "none";
} /*隐藏的编辑框*/

function showIncome(){
	document.getElementById('income').style.display="block";
	document.getElementById('in_finance').getElementsByTagName('p')[0].className="on";
	document.getElementById('in_finance').getElementsByTagName('p')[1].className="";
	document.getElementById('outlay').style.display = "none";
	}
function showOutlay(show){
	document.getElementById('outlay').style.display="block";
	document.getElementById('in_finance').getElementsByTagName('p')[1].className="on";
	document.getElementById('in_finance').getElementsByTagName('p')[0].className="";
	document.getElementById('income').style.display="none";
}
function CheckNull(element) {
    var warning= document.getElementById(element.id + 'Span');
    if (element.value == "") {
        warning.style.visibility = 'visible';
        return false;
    }
    else {
        warning.visibility = 'hidden';
        return true;
    }
}
function CheckAll() {
    if ((CheckNull(document.getElementById('principle_TextBox1')) & CheckNull(document.getElementById('project_name')) & CheckNull(document.getElementById('bailer')) & CheckNull(document.getElementById('money')))!=0)
  return true;
    else return false;
}
function CheckTwo(e1, e2) {
    if ((CheckNull(e1) & CheckNull(e2) & isDigit(e2, document.getElementById(e2.id+'Span'), 0)) == 0)
        return false;
    else return true;
}

var newTime = new Date;
var year = document.getElementById('year');
var month = document.getElementById('month');
var day = document.getElementById('day');
year.value = newTime.getFullYear();
document.getElementById('get_year').value = newTime.getFullYear();
month.value = newTime.getMonth() + 1;
document.getElementById('get_month').value = newTime.getMonth() + 1;
day.value = newTime.getDate();
document.getElementById('get_day').value = newTime.getDate();

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
document.getElementById('principle_TextBox1').setAttribute('onblur', 'CheckNull(this)');

/*中间页面的变换*/
getFinance = document.getElementById('financeBtn');
getNotice = document.getElementById('noticeBtn');
getProject = document.getElementById('project').parentNode;
getMidUl = document.getElementById('middle').getElementsByTagName('ul')[0];
getComplete = document.getElementById('completedBtn');
function changeFinance() {
    getProject.id = 'mid_heading' + '_financial';
    getMidUl.id = 'mid_list' + '_financial';
    document.getElementById('notComeFinanceSpan').style.display = 'block';
    if (document.getElementById('userRoleHi').value != "财务管理") {  getComplete.value = ""; getShowEdit.innerHTML = "";document.getElementById('notComeFinanceSpan').style.border = 'none';}
    else { getComplete.value = "发放"; getShowEdit.innerHTML = "添加"; document.getElementById('deleteFinanceSpan').style.display = 'block'; };
    removed = getProject.getElementsByTagName('span');
    removed[0].innerHTML = "总额";
    removed[1].innerHTML = "收支时间";
    if (removed.length == 3)
        removed[removed.length - 1].style.display = 'none';
}
function changeNotice () {
    getProject.id = 'mid_heading' + '_notice';
    getMidUl.id = 'mid_list' + '_notice';
    getShowEdit.innerHTML = "发布";
    document.getElementById('notComeFinanceSpan').style.display = 'none';
    document.getElementById('deleteFinanceSpan').style.display = 'none';
    if (document.getElementById('userRoleHi').value != "负责人") getComplete.value = "";
    else getComplete.value = "删除";
    removed = getProject.getElementsByTagName('span');
    removed[0].innerHTML = "发布人";
    removed[1].innerHTML = "发布时间";
    if (removed.length == 3)
        removed[removed.length - 1].style.display = 'none';
} 
function isDigit(e, numSpan,maxNum) {
    var patrn = /^[0-9]{1,20}$/;
    if (!patrn.exec(e.value) && e.value.length != 0) {numSpan.innerText = '只能是数字';numSpan.style.visibility = 'visible'; return false; }
    else if (maxNum != 0 && parseInt(e.value) > maxNum) {numSpan.innerText = '不能大于' + maxNum.toString();numSpan.style.visibility = 'visible';return false; }
    else return true;
}
var midListId=document.getElementById('mid_list').getElementsByTagName('ul')[0].id;
if (midListId == 'NoticeMidList1_mid_list_notice')
    changeNotice();
else if (midListId == 'FinancialMidList1_mid_list_financial') changeFinance();