import { Component, Input } from '@angular/core';

@Component({
  selector: 'base-loading',
  templateUrl: './base-loading.component.html',
  styleUrls: ['./base-loading.component.css'],
})
export class BaseLoadingComponent {
  @Input() size: string = 'sm';
}
