import { Injectable } from "@angular/core";

@Injectable()
export class DeviceService {
  constructor() {}

  userDeviceControl() {
    if (navigator.userAgent.toLowerCase().indexOf("android") == -1
    && navigator.userAgent.toLowerCase().indexOf("iphone") == -1
    ) {
       window.location.replace("http://site.neyapalim.online");
    }
    
  }
}
