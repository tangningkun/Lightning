/* jshint esversion: 6 */
define(['main'], function(main) {
  var module = {
    /** 判断字符串是否为空 */
    _IsEmpty: function(obj) {
      if (typeof obj == 'undefined' || obj == null || obj == '') {
        return true;
      } else {
        return false;
      }
    },
    /**
     * 获取未加密数据
     * @param {地址} url
     * @param {GET|POST} type
     * @param {数据} data
     * @param {token} token
     */
    _HttpAjax: function(url, type, data, async) {
      return $.ajax({
        url: url,
        contentType: 'application/x-www-form-urlencoded',
        data: data,
        type: type,
        async: async,
        success: function(msg) {
          return msg;
        },
        error: function(e) {
          return e;
        }
      });
    },
    /**
     *获取加密数据
     * @param {地址} url
     * @param {GET|POST} type
     * @param {数据} data
     * @param {是否异步} async
     * @param {token} token
     */
    _HttpAjaxToken: function(url, type, data, async, token) {
      let json_web_token = 'Bearer ' + token;
      return $.ajax({
        url: url,
        contentType: 'application/x-www-form-urlencoded',
        beforeSend: function(request) {
          request.setRequestHeader('Authorization', json_web_token);
        },
        data: data,
        type: type,
        async: async,
        success: function(msg) {
          return msg;
        },
        error(e) {
          return e;
        }
      });
    }
  };
  return module;
});
