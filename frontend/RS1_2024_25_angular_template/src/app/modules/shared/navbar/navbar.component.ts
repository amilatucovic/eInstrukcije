import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  @Output() tabChange = new EventEmitter<string>();

  onTabClick(tabName: string, event: Event) {
    event.preventDefault();
    this.tabChange.emit(tabName);
  }
}
