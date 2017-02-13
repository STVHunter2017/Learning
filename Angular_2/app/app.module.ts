import {NgModule} from '@angular/core'
import {BrowserModule} from '@angular/platform-browser'
import {EventsAppComponent} from './events-app.component'
import {EventsListComponent} from './events/events-list.component'
import {EventThumnailComponent} from './events/event-thumbnail.component'
import {NavbarComponent} from './Nav/navbar.component'
import {EventDetailsComponent} from './events/event-details/event-details.component'
@NgModule({
    imports : [BrowserModule],
    declarations : [EventsAppComponent, EventsListComponent, EventThumnailComponent, NavbarComponent, EventDetailsComponent],
    bootstrap:  [EventsAppComponent]

})

export class AppModule{}