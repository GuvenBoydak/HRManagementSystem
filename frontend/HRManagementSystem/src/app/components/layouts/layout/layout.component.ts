import { Component } from '@angular/core';
import { SharedModule } from '../../../common/shared/shared/shared.module';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-layout',
  imports: [SharedModule,NavbarComponent],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {

}
