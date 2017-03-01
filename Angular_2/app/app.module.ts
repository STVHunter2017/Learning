import {NgModule} from '@angular/core'
import {BrowserModule} from '@angular/platform-browser'
import {RouterModule} from '@angular/router'

import {EventsAppComponent} from './events-app.component'

import {NavbarComponent} from './Nav/navbar.component'
import {appRoutes} from './routes'
import {Error404Component} from './errors/404.component'
import {ToastrService} from './common/toastr-service'

import {
    EventsListComponent,
    EventThumnailComponent,    
    EventService,
    EventDetailsComponent,    
    CreateEventComponent,
    EventRouteActivator,
    EventsListResolverService
} from './events/index'


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