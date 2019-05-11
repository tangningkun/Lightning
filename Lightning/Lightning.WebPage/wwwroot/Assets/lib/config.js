var appPath = appPath || '/';
var timeStamp = _version || new Date().getTime();

// 判断浏览器
// IE9以下
function getFirstMatch(regexp, ua) {
  var match = ua.match(regexp);
  return (match && match.length > 0 && match[1]) || '';
}

var version = getFirstMatch(/(?:msie |rv:)(\d+(\.?_?\d+)+)/i, navigator.userAgent);
if (version && parseInt(version) < 10) {
  window.location.href = '/compatibility.html';
}
requirejs.config({
  baseUrl: '/assets',
  paths: {
    main: 'app/main',
    current: 'app/current',
    // core libary
    jquery: 'node_modules/jquery/dist/jquery.min',
    bootstrapvalidator: 'node_modules/bootstrapvalidator/dist/js/bootstrapValidator.min',
    // requireJs plugins
    depend: 'lib/requirejs-plugins/depend',
    text: 'lib/requirejs-plugins/text',
    lay: 'lib/requirejs-plugins/lay',
    // javascript shim
    es6: 'node_modules/requirejs-babel/es6',
    babel: 'node_modules/requirejs-babel/babel-5.8.34.min',
    polyfill: 'node_modules/babel-polyfill/dist/polyfill.min',
    md5: 'node_modules/blueimp-md5/js/md5.min',
    jwt: 'node_modules/jsonwebtoken/index'
  },
  urlArgs: 'v=' + timeStamp
});
requirejs(['polyfill'], function() {
  //if (islogin)
  //    requirejs([_jsPath || 'app/login']);
  requirejs([_jsPath || 'app/main']);
});
