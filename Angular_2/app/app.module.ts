import {NgModule} from '@angular/core'
import {BrowserModule} from '@angular/platform-browser'
import {RouterModule} from '@angular/router'

import {EventsAppComponent} from './events-app.component'
import {EventsListComponent} from './events/events-list.component'
import {EventThumnailComponent} from './events/event-thumbnail.component'
import {NavbarComponent} from './Nav/navbar.component'
import {EventDetailsComponent} from './events/event-details/event-details.component'
import {EventService} from './events/shared/event.service'
import {appRoutes} from './routes'
import {CreateEventComponent} from './events/create-event-component'
import {Error404Component} from './errors/404.component'
import {EventRouteActivator} from './events/event-details/event-route-activator.service'
import {ToastrService} from './common/toastr-service'
import {EventsListResolverService} from './events/shared/events-list.resolver.service'
@NgModule({
    imports : [BrowserModule, RouterModule.forRoot(appRoutes)],
    declarations : [EventsAppComponent, EventsListComponent, EventThumnailComponent, NavbarComponent, EventDetailsComponent, CreateEventComponent,Error404Component],    
    providers: [EventRouteActivator, 
                EventService,
                EventsListResolverService,
                ToastrService,                
                {
                    provide: 'canDeactivateCreateEvent', useValue: checkDirtyState
                }
                ],
    bootstrap:  [EventsAppComponent]

})

export class AppModule{}


function checkDirtyState(component: CreateEventComponent){
    if (component.isDirty)
        return window.confirm('You have not saved this event, do you really want to cancel?')

    return true;
}