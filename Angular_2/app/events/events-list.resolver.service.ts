import {Injectable} from '@angular/core'
import {Resolve} from '@angular/router'
import {EventService} from './shared/event.service'

@Injectable()
export class EventsListResolverService implements Resolve<any>{
    constructor(private eventService:EventService){

    }
    resolve(){
        return this.eventService.getEvents().map(events=>events)  //Short for events=> return events
    }

}

//Normally you would use subscribe to get data from an observable.
//However, the resolver does this, so it needs the data in a observable hence the map.  Resolve monitors the observable to see when it has finished loading