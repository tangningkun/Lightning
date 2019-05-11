/* jshint esversion: 6 */
define(['main', 'current', 'md5', 'jwt', 'lay!layer'], function(main, current, md5, jwt) {
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
          Password: md5(password)
        },
        type: 'POST',
        async: true,
        success: function(result) {
          if (result.code == 200) {
            let token_url = window.webapi + 'Lightning/GetJsonWebToken';
            current
              ._HttpAjax(
                token_url,
                'POST',
                {
                  UserName: username,
                  Password: md5(password)
                },
                false
              )
              .then(res => {
                debugger;
                console.log(res);
                //npm install jsonwebtoken
                let token = jwt(res);
                location.href = '/Home/Index'; //跳转到首页
              })
              .catch(err => {
                console.log('token', err);
              });
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
