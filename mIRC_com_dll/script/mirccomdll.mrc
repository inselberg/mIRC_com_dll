;
; load -rs mirccomdll.mrc
; mirc remote-script ... add it to mirc and test it with /co /status
; use it to download linux.iso files or other legal stuff...
;

on 1:FILERCVD:*.*:{
  echo -s 7 $+ Received $filename 
  echo -s 7 $+ next?
  %result = $com(lmirc,isLast,1)
  if ( $com(lmirc).result == $false ) {
    //var %result = $com(lmirc,Status,1)
    echo -s 7  $+ $com(lmirc).result

    //var %result = $com(lmirc,Next,1)
    echo -s 7  $+ $com(lmirc).result
    $com(lmirc).result
  }
}

alias co  {
  echo 0,7 ComOpen: mIrcComDll.Listen
  comopen lmirc mIrcComDll.Listen
  var %result = $com(lmirc,Version,1)
  echo 0,7 $+ Version: $com(lmirc).result
}

alias cc  {
  echo 7 ComClose: mIrcComDll.Listen
  comclose lmirc mIrcComDll.Listen
}

alias status {
  //var %result = $com(lmirc,Status,1)
  echo 7 $+ $com(lmirc).result
}

alias add {
  //var %result = $com(lmirc,Add,1,bstr,$1-)
  echo 7 $+ Added: $com(lmirc).result
}

alias first {
  //var %result = $com(lmirc,First,1)
  echo 7 $+ First: $com(lmirc).result
  $com(lmirc).result
}

alias current {
  //var %result = $com(lmirc,curent,1)
  echo 7 $+ current: $com(lmirc).result
  ;  $com(lmirc).result
}

alias next {
  //var %result = $com(lmirc,next,1)
  echo 7 $+ next: $com(lmirc).result
  $com(lmirc).result
}

alias clear {
  //var %result = $com(lmirc,clear,1)
  echo 7 $+ Cleared List: $com(lmirc).result
  $com(lmirc).result
}

alias saveit {
  //var %result = $com(lmirc,save,1,bstr,c:\mirc.queue.txt)
  echo 7 $+ saved...: $com(lmirc).result
}

alias loadit {
  //var %result = $com(lmirc,load,1,bstr,c:\mirc.queue.txt)
  echo 7 $+ loaded...: $com(lmirc).result
}

alias insert {
  //var %result = $com(lmirc,Insert,1,bstr,$1-)
  echo 7 $+ Insert: $com(lmirc).result
}

alias delete {
  //var %result = $com(lmirc,Delete,1,int,$1-)
  echo 7 $+ Delete: $com(lmirc).result
}
