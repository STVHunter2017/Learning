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
import {CreateEvent} from './events/create-event-component'

@NgModule({
    imports : [BrowserModule, RouterModule.forRoot(appRoutes)],
    declarations : [EventsAppComponent, EventsListComponent, EventThumnailComponent, NavbarComponent, EventDetailsComponent, CreateEvent],    
    providers: [EventService],
    bootstrap:  [EventsAppComponent]

})

export class AppModule{}