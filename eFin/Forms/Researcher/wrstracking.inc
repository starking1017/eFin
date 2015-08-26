<!-- START OF SDC Advanced Tracking Code -->
<!-- Copyright (c) 1996-2005 WebTrends Inc.  All rights reserved. -->
<!-- V7.5 -->
<!-- $DateTime: 2005/09/22 12:07:59 $ -->
<SCRIPT LANGUAGE="Javascript"><!--
var gTimeZone = 0;
// Code section for Enable First-Party Cookie Tracking
function dcsGetCookie(name){
	var pos=document.cookie.indexOf(name+"=");
	if (pos!=-1){
		var start=pos+name.length+1;
		var end=document.cookie.indexOf(";",start);
		if (end==-1){
			end=document.cookie.length;
		}
		return unescape(document.cookie.substring(start,end));
	}
	return null;
}
function dcsGetCrumb(name,crumb){
	var aCookie=dcsGetCookie(name).split(":");
	for (var i=0;i<aCookie.length;i++){
		var aCrumb=aCookie[i].split("=");
		if (crumb==aCrumb[0]){
			return aCrumb[1];
		}
	}
	return null;
}
function dcsGetIdCrumb(name,crumb){
	var cookie=dcsGetCookie(name);
	var id=cookie.substring(0,cookie.indexOf(":lv="));
	var aCrumb=id.split("=");
	for (var i=0;i<aCrumb.length;i++){
		if (crumb==aCrumb[0]){
			return aCrumb[1];
		}
	}
	return null;
}
function dcsFPC(offset){
	if (typeof(offset)=="undefined"){
		return;
	}
	var name=gFpc;
	var dCur=new Date();
	dCur.setTime(dCur.getTime()+(dCur.getTimezoneOffset()*60000)+(offset*3600000));
	var dExp=new Date(dCur.getTime()+315360000000);
	var dSes=new Date(dCur.getTime());
	if (document.cookie.indexOf(name+"=")!=-1){
		var id=dcsGetIdCrumb(name,"id");
		var lv=parseInt(dcsGetCrumb(name,"lv"));
		var ss=parseInt(dcsGetCrumb(name,"ss"));
		if ((id==null)||(id=="null")||isNaN(lv)||isNaN(ss)){
			return;
		}
		WT.co_f=id;
		var dLst=new Date(lv);
		dSes.setTime(ss);
		if ((dCur.getTime()>(dLst.getTime()+1800000))||(dCur.getTime()>(dSes.getTime()+28800000))){
			dSes.setTime(dCur.getTime());
			WT.vt_f_s="1";
		}
		if ((dCur.getDay()!=dLst.getDay())||(dCur.getMonth()!=dLst.getMonth())||(dCur.getYear()!=dLst.getYear())){
			WT.vt_f_d="1";
		}
	}
	else{
		var tmpname=name+"_TMP=";
		document.cookie=tmpname+"1";
		if (document.cookie.indexOf(tmpname)!=-1){
			document.cookie=tmpname+"; expires=Thu, 01-Jan-1970 00:00:01 GMT";
			if ((typeof(gWtId)!="undefined")&&(gWtId!="")){
				WT.co_f=gWtId;
			}
			else if ((typeof(gTempWtId)!="undefined")&&(gTempWtId!="")){
				WT.co_f=gTempWtId;
				WT.vt_f="1";
			}
			else{
				WT.co_f="2";
				var cur=dCur.getTime().toString();
				for (var i=2;i<=(32-cur.length);i++){
					WT.co_f+=Math.floor(Math.random()*16.0).toString(16);
				}
				WT.co_f+=cur;
				WT.vt_f="1";
			}
			if (typeof(gWtAccountRollup)=="undefined"){
				WT.vt_f_a="1";
			}
			WT.vt_f_s="1";
			WT.vt_f_d="1";
		}
		else{
			WT.vt_f="2";
			WT.vt_f_a="2";
			return;
		}
	}
	WT.co_f=escape(WT.co_f);
	WT.vt_sid=WT.co_f+"."+dSes.getTime();
	var expiry="; expires="+dExp.toGMTString();
	document.cookie=name+"="+"id="+WT.co_f+":lv="+dCur.getTime().toString()+":ss="+dSes.getTime().toString()+expiry+"; path=/"+(((typeof(gFpcDom)!="undefined")&&(gFpcDom!=""))?("; domain="+gFpcDom):(""));
}

// Code section for Use the new first-party cookie generated with this tag.
var gFpc="WT_FPC";
var gWtId="";
var gTempWtId="";
var gConvert=true;

// Code section for Enable SmartView Transition Page tracking
function dcsTP(){
	var name="WT_DC";
	var expiry="; expires=Thu, 31-Dec-2020 08:00:00 GMT";
	var path="; path=/";
	var domain="";
	if ((document.cookie.indexOf(name+"=")!=-1)&&(dcsGetCrumb(name,"tsp")=="1")){
		WT.ttp="1";
	}
	if (dcsGetMeta("SmartView_Page")=="1"){
		WT.tsp="1";
		document.cookie=name+"=tsp=1"+expiry+path+domain;
	}
	else{
		document.cookie=name+"=; expires=Sun, 1-Jan-1995 00:00:00 GMT;"+path+domain;
	}
}
function dcsGetMeta(name){
	var elems;
	if (document.all){
		elems=document.all.tags("meta");
	}
	else if (document.documentElement){
		elems=document.getElementsByTagName("meta");
	}
	if (typeof(elems)!="undefined"){
		for (var i=1;i<=elems.length;i++){
			var meta=elems.item(i-1);
			if (meta.name&&(meta.name.indexOf(name)==0)){
				return meta.content;
				break;
			}
		}
	}
	return null;
}
//-->
</SCRIPT>
<!-- END OF SDC Advanced Tracking Code -->
<!-- START OF SmartSource Data Collector TAG -->
<!-- Copyright (c) 1996-2005 WebTrends Inc. All rights reserved. -->
<!-- V7.5 -->
<!-- $DateTime: 2005/09/22 12:07:59 $ -->
<SCRIPT LANGUAGE="Javascript"><!--
gVersion="1.0";
//-->
</SCRIPT>
<SCRIPT LANGUAGE="Javascript1.1"><!--
gVersion="1.1";
//-->
</SCRIPT>
<SCRIPT LANGUAGE="Javascript1.2"><!--
gVersion="1.2";
var RE={"%09":/\t/g, "%20":/ /g, "%23":/\#/g,"%26":/\&/g,"%2B":/\+/g,"%3F":/\?/g,"%5C":/\\/g};
//-->
</SCRIPT>
<SCRIPT LANGUAGE="Javascript1.3"><!--
gVersion="1.3";
//-->
</SCRIPT>
<SCRIPT LANGUAGE="Javascript1.4"><!--
gVersion="1.4";
//-->
</SCRIPT>
<SCRIPT LANGUAGE="Javascript1.5"><!--
gVersion="1.5";
//-->
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript"><!--
var gImages=new Array;
var gIndex=0;
var DCS=new Object();
var WT=new Object();
var DCSext=new Object();

var gDomain="statse.webtrendslive.com";
var gDcsId="dcsb35jrt0000047kjrmwcz5x_1g3c";

if ((typeof(gConvert)!="undefined")&&gConvert&&(document.cookie.indexOf(gFpc+"=")==-1)){
	document.write("<SCR"+"IPT Language='JavaScript' SRC='"+"http"+(window.location.protocol.indexOf('https:')==0?'s':'')+"://"+gDomain+"/"+gDcsId+"/wtid.js"+"'></SCR"+"IPT>");
}
//-->
</SCRIPT>
<SCRIPT LANGUAGE="Javascript"><!--
function dcsVar(){
	var dCurrent=new Date();
	WT.tz=dCurrent.getTimezoneOffset()/60*-1;
	if (WT.tz==0){
		WT.tz="0";
	}
	WT.bh=dCurrent.getHours();
	WT.ul=navigator.appName=="Netscape"?navigator.language:navigator.userLanguage;
	if (typeof(screen)=="object"){
		WT.cd=navigator.appName=="Netscape"?screen.pixelDepth:screen.colorDepth;
		WT.sr=screen.width+"x"+screen.height;
	}
	if (typeof(navigator.javaEnabled())=="boolean"){
		WT.jo=navigator.javaEnabled()?"Yes":"No";
	}
	if (document.title){
		WT.ti=document.title;
	}
	WT.js="Yes";
	if (typeof(gVersion)!="undefined"){
		WT.jv=gVersion;
	}
	WT.sp="62991";
	DCS.dcsdat=dCurrent.getTime();
	DCS.dcssip=window.location.hostname;
	DCS.dcsuri=window.location.pathname;
	if (window.location.search){
		DCS.dcsqry=window.location.search;
	}
	if ((window.document.referrer!="")&&(window.document.referrer!="-")){
		if (!(navigator.appName=="Microsoft Internet Explorer"&&parseInt(navigator.appVersion)<4)){
			DCS.dcsref=window.document.referrer;
		}
	}
}

function A(N,V){
	return "&"+N+"="+dcsEscape(V);
}

function dcsEscape(S){
	if (typeof(RE)!="undefined"){
		var retStr = new String(S);
		for (R in RE){
			retStr = retStr.replace(RE[R],R);
		}
		return retStr;
	}
	else{
		return escape(S);
	}
}

function dcsCreateImage(dcsSrc){
	if (document.images){
		gImages[gIndex]=new Image;
		gImages[gIndex].src=dcsSrc;
		gIndex++;
	}
	else{
		document.write('<IMG BORDER="0" NAME="DCSIMG" WIDTH="1" HEIGHT="1" SRC="'+dcsSrc+'">');
	}
}

function dcsMeta(){
	var elems;
	if (document.all){
		elems=document.all.tags("meta");
	}
	else if (document.documentElement){
		elems=document.getElementsByTagName("meta");
	}
	if (typeof(elems)!="undefined"){
		for (var i=1;i<=elems.length;i++){
			var meta=elems.item(i-1);
			if (meta.name){
				if (meta.name.indexOf('WT.')==0){
					WT[meta.name.substring(3)]=meta.content;
				}
				else if (meta.name.indexOf('DCSext.')==0){
					DCSext[meta.name.substring(7)]=meta.content;
				}
				else if (meta.name.indexOf('DCS.')==0){
					DCS[meta.name.substring(4)]=meta.content;
				}
			}
		}
	}
}

function dcsTag(){
	var P="http"+(window.location.protocol.indexOf('https:')==0?'s':'')+"://"+gDomain+(gDcsId==""?'':'/'+gDcsId)+"/dcs.gif?";
	for (N in DCS){
		if (DCS[N]) {
			P+=A(N,DCS[N]);
		}
	}
	for (N in WT){
		if (WT[N]) {
			P+=A("WT."+N,WT[N]);
		}
	}
	for (N in DCSext){
		if (DCSext[N]) {
			P+=A(N,DCSext[N]);
		}
	}
	if (P.length>2048&&navigator.userAgent.indexOf('MSIE')>=0){
		P=P.substring(0,2040)+"&WT.tu=1";
	}
	dcsCreateImage(P);
}

dcsVar();
dcsMeta();
dcsFPC(gTimeZone);
dcsTP();
dcsTag();
//-->
</SCRIPT>

<NOSCRIPT>
<IMG ALT="" BORDER="0" NAME="DCSIMG" WIDTH="1" HEIGHT="1" SRC="http://statse.webtrendslive.com/dcsb35jrt0000047kjrmwcz5x_1g3c/njs.gif?dcsuri=/nojavascript&amp;WT.js=No">
</NOSCRIPT>
<!-- END OF SmartSource Data Collector  TAG -->	
