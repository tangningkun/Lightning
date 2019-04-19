define(['main'], function(main) {
  console.log('window.webapi', window.webapi);
  $.ajax({
    url: window.webapi + '/Lightning/GetAllDepartment',
    contentType: 'application/x-www-form-urlencoded',
    data: {},
    type: 'GET',
    async: false,
    success: function(result) {
      console.log('result', result);
    }
  });
  //获取用户Session
  console.log('usersession', JSON.parse(usersession.replace(/&quot;/g, '"')));
});
