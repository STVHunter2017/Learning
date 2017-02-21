import {CanActivate, ActivatedRouteSnapshot,Router} from '@angular/router'
import {Injectable} from '@angular/core'
import {EventService} from '../shared/event.service'


@Injectable()
export class EventRouteActivator implements CanActivate {
    constructor(private eventService: EventService, private router: Router){

    }

    canActivate(route:ActivatedRouteSnapshot){
        const eventExists = !!this.eventService.getEvent(+route.params['id'])

        if (!eventExists) 
            this.router.navigate(['/404'])
        
        return eventExists;
    }
}

//+ cast event id to a number
//!! - don't know what this is yet.  Looks like somesort of coalasce operator