import { Component, Input, OnInit } from '@angular/core';
import { Globals } from 'src/app/globals';

@Component({
  selector: 'base-image',
  templateUrl: './base-image.component.html',
  styleUrls: ['./base-image.component.css'],
})
export class BaseImageComponent implements OnInit {
  @Input() imageUrl: string = '';
  @Input() altText: string = 'Image';
  @Input() imageClass: string = 'img-fluid';

  imgUrl: string = '';

  constructor() {}

  ngOnInit() {
    this.imgUrl = Globals.s3BaseUrl + this.imageUrl;
  }
}
