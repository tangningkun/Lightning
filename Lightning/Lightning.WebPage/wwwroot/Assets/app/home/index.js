/* jshint esversion: 6 */
define(['main', 'current'], function(main, current) {
  console.log('window.webapi', window.webapi);
  let token = 'Bearer' + ' ' + current._getCookie('token');
  console.log('token', token);
  let url = window.webapi + 'Lightning/GetAllDepartment';
  /* current._HttpAjax(url, 'POST', {}, false).then(result => {
    console.log('result', result);
  }); */

  /* $.ajax({
    url: window.webapi + 'Lightning/GetAllDepartment',
    contentType: 'application/x-www-form-urlencoded',
    data: {},
    type: 'POST',
    async: false,
    success: function(result) {
      console.log('result', result);
    }
  }); */

  $.ajax({
    url: window.webapi + 'Lightning/GetAll',
    type: 'POST',
    beforeSend: function(request) {
      request.setRequestHeader('authorization', token);
    },
    contentType: 'application/json',
    async: false,
    success: function(result) {
      console.log('result', result);
    },
    error(e) {
      console.log(e);
    }
  });
  //获取用户Session
  //console.log('usersession', JSON.parse(usersession.replace(/&quot;/g, '"')));
});
