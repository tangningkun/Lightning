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
    _HttpAjax: function(url, type, data, isasync, contentType = 'application/x-www-form-urlencoded') {
      return $.ajax({
        url: url,
        contentType: contentType,
        data: data,
        type: type,
        async: isasync,
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
        contentType: 'application/json',
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
    },
    /**
     *获取token
     *@param {地址} url
     * @param {GET|POST} type
     * @param {数据} data
     */
    _HttpGetToken: function(url, type, data) {
      return $.ajax({
        url: url,
        contentType: 'application/json',
        beforeSend: function(request) {
          request.setRequestHeader('Content-Type', 'application/json');
        },
        data: data,
        type: type,
        dataType: 'json',
        async: false,
        success: function(msg) {
          return msg;
        },
        error(e) {
          return e;
        }
      });
    },
    /**
     * 获取cookie
     * @param {cookie名} c_name
     */
    _getCookie: function(c_name) {
      if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + '=');
        if (c_start != -1) {
          c_start = c_start + c_name.length + 1;
          c_end = document.cookie.indexOf(';', c_start);
          if (c_end == -1) c_end = document.cookie.length;
          return unescape(document.cookie.substring(c_start, c_end));
        }
      }
      return '';
    }
  };
  return module;
});
