/* jshint esversion: 6 */
define(['main', 'current'], function(main, current) {
  console.log('window.webapi', window.webapi);
  let url = window.webapi + 'Lightning/GetAllDepartment';
  current._HttpAjax(url, 'POST', {}, false).then(result => {
    console.log('result', result);
  });

  $.ajax({
    url: window.webapi + 'Lightning/GetAllDepartment',
    contentType: 'application/x-www-form-urlencoded',
    data: {},
    type: 'POST',
    async: false,
    success: function(result) {
      console.log('result', result);
    }
  });

  $.ajax({
    url: window.webapi + 'Lightning/Get',
    contentType: 'application/x-www-form-urlencoded',
    beforeSend: function(request) {
      request.setRequestHeader(
        'Authorization',
        'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJleHAiOjE1NTc0Njk4MzIsImlzcyI6IlRhbmdOaW5nS3VuSXNzdWVyIiwiYXVkIjoiVGFuZ05pbmdLdW5BdWRpZW5jZSJ9.Bcj-4IYEmrs8oIo7OwM64CPXnSGXGxaufsb9PJMPaNg'
      );
    },
    data: { id: 1 },
    type: 'GET',
    async: false,
    success: function(result) {
      console.log('result', result);
    }
  });
  //获取用户Session
  //console.log('usersession', JSON.parse(usersession.replace(/&quot;/g, '"')));
});
