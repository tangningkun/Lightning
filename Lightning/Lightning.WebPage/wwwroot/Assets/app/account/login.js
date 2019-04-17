/* jshint esversion: 6 */
define(['main', 'current', 'md5', 'lay!layer'], function(main, current, md5) {
  console.log(md5('123456'));
  var layer = layui.layer;
  var module = {
    _init: function(username, password) {
      if (module._checkform(username, password)) {
        return;
      }
      module._checklogin(username, password);
    },
    _checkform: function(username, password) {
      if (current._IsEmpty(username)) {
        layer.msg('用户名不能为空', { icon: 2 });
        return true;
      }
      if (current._IsEmpty(password)) {
        layer.msg('密码不能为空', { icon: 2 });
        return true;
      }
      return false;
    },
    _checklogin: function(username, password) {
      $.ajax({
        url: '/Account/CheckLogin',
        contentType: 'application/x-www-form-urlencoded',
        data: {
          UserName: username,
          Password: password
        },
        type: 'POST',
        async: true,
        success: function(result) {
          console.log('result', result);
          if (result.code == 200) {
            location.href = '/Home/Index'; //跳转到首页
          } else if (result.code != 202) {
            layer.msg(result.message, { icon: 2 });
          } else {
            layer.msg('系统错误,请重新登录！', { icon: 2 });
          }
        }
      });
    }
  };
  $('#btn-login').click(function() {
    let UserName = $('#form-username')
      .val()
      .trim();
    let PsssWord = $('#form-password')
      .val()
      .trim();
    module._init(UserName, PsssWord);
  });
});
