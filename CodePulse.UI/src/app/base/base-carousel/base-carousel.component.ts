import { Component, Input, OnInit } from '@angular/core';
import { Globals } from 'src/app/globals';

@Component({
  selector: 'base-carousel',
  templateUrl: './base-carousel.component.html',
  styleUrls: ['./base-carousel.component.css'],
})
export class BaseCarouselComponent implements OnInit {
  @Input() images: string[] = [];

  get imagesUrl(): string[] {
    return this.images.map((img) => Globals.s3BaseUrl + img);
  }

  isHovered: boolean = false;
  currentImage: string = '';
  currentIndex: number = -1;
  interval: any;

  ngOnInit(): void {
    this.currentImage = this.imagesUrl[0];
    this.startInterval();
  }

  startInterval(): void {
    this.interval = setInterval(() => {
      this.next();
    }, 3000);
  }

  stopInterval(): void {
    clearInterval(this.interval);
  }

  onMouseEnter(): void {
    this.isHovered = true;
    this.stopInterval();
  }

  onMouseLeave(): void {
    this.isHovered = false;
    this.startInterval();
  }

  changeImage(image: string): void {
    this.currentImage = image;
    this.currentIndex = this.imagesUrl.indexOf(image);
    this.stopInterval();
  }

  next(): void {
    this.currentIndex = (this.currentIndex + 1) % this.imagesUrl.length;
    this.currentImage = this.imagesUrl[this.currentIndex];
  }

  prev(): void {
    this.currentIndex =
      (this.currentIndex - 1 + this.imagesUrl.length) % this.imagesUrl.length;
    this.currentImage = this.imagesUrl[this.currentIndex];
  }
}
