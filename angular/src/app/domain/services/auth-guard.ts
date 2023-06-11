import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, CanDeactivate, CanLoad, Route, Router, RouterStateSnapshot, UrlSegment, UrlTree } from "@angular/router";
import { AuthenticationService } from "./authentication.service";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { RoleType } from "../model/schemas";

@Injectable({
    providedIn: 'root'
  })
  export class AuthGuard implements CanActivate, CanActivateChild, CanDeactivate<unknown>, CanLoad {
  
    constructor(private authenticationService: AuthenticationService, private router: Router) { }
  
    canActivate(
      next: ActivatedRouteSnapshot,
      state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      let url: string = state.url;
      return this.checkUserLogin(next, url);
    }
    canActivateChild(
      next: ActivatedRouteSnapshot,
      state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      return this.canActivate(next, state);
    }
    canDeactivate(
      component: unknown,
      currentRoute: ActivatedRouteSnapshot,
      currentState: RouterStateSnapshot,
      nextState?: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      return true;
    }
    canLoad(
      route: Route,
      segments: UrlSegment[]): Observable<boolean> | Promise<boolean> | boolean {
      return true;
    }
  
    checkUserLogin(route: ActivatedRouteSnapshot, url: any): boolean {
      if (AuthenticationService.CurrentUser) {
        const userRole = AuthenticationService.CurrentUser.role;
        if (route.data['role'] && route.data['role'].indexOf(RoleType[userRole]) === -1) {
          this.router.navigate(['/']);
          return false;
        }
        
        return true;
      }
  
      this.router.navigate(['/']);
      return false;
    }
  }