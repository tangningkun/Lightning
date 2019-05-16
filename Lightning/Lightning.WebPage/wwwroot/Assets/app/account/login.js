/* jshint esversion: 6 */
define(['main', 'current', 'md5', 'dayjs', 'lay!layer'], function(main, current, md5, dayjs) {
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
      let data = {
        UserName: username,
        Password: md5(password)
      };
      current
        ._HttpAjax('/Account/CheckLogin', 'POST', data)
        .then(result => {
          if (result.code == 200) {
            module._getToken(data);
          } else if (result.code != 202) {
            layer.msg(result.message, { icon: 2 });
          } else {
            layer.msg('系统错误,请重新登录！', { icon: 2 });
          }
        })
        .catch(e => {
          layer.msg('系统故障！', { icon: 2 });
        });
    },
    _getToken: function(data) {
      let token_url = window.webapi + 'Lightning/GetJsonWebToken';
      current
        ._HttpGetToken(token_url, 'POST', JSON.stringify(data))
        .then(msg => {
          if (msg.code == '200') {
            document.cookie = 'token=' + msg.token + ';expires=' + new Date(Date.parse(new Date()) + 30 * 60 * 1000).toUTCString();
            location.href = '/Home/Index'; //跳转到首页
          }
        })
        .catch(e => {
          //module._getToken(data);
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
