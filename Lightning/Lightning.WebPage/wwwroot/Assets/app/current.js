define(['main'], function(main) {
  var module = {
    /** 判断字符串是否为空 */
    _IsEmpty: function(obj) {
      if (typeof obj == 'undefined' || obj == null || obj == '') {
        return true;
      } else {
        return false;
      }
    }
  };
  return module;
});
