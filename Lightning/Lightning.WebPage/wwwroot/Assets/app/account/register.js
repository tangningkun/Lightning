/* jshint esversion: 6 */
define(['main', 'current', 'lay!layer'], function(main, current) {
  var layer = layui.layer;
  var module = {
    _init: function(data) {
      console.log(data);
      if (module._checkform(data)) {
        return;
      }
      module._registereduser(data);
    },
    _checkform: function(data) {
      if (current._IsEmpty(data.UserName)) {
        layer.msg('用户名不能为空', { icon: 2 });
        return true;
      }
      if (current._IsEmpty(data.Password)) {
        layer.msg('密码不能为空', { icon: 2 });
        return true;
      }
      if (current._IsEmpty(data.ConfirmPassword)) {
        layer.msg('确认密码不能为空', { icon: 2 });
        return true;
      }
      return false;
    },
    _registereduser: function(data) {
      $.ajax({
        url: '/Account/RegisteredUser',
        contentType: 'application/x-www-form-urlencoded',
        data: data,
        type: 'POST',
        async: false,
        success: function(result) {
          if (result.code == 200) {
            layer.msg('注册成功！', { icon: 1 });
            location.href = '/Account/Login'; //跳转到首页
          } else if (result.code != 202) {
            layer.msg(result.message, { icon: 2 });
          } else {
            layer.msg('系统错误,请重新登录！', { icon: 2 });
          }
        }
      });
    }
  };
  $('#btn-register').click(function() {
    let UserName = $('#form-username')
      .val()
      .trim();
    let PsssWord = $('#form-password')
      .val()
      .trim();
    let ConfirmPsssWord = $('#form-confirmpassword')
      .val()
      .trim();
    module._init({ UserName: UserName, Password: PsssWord, ConfirmPassword: ConfirmPsssWord });
  });
});
