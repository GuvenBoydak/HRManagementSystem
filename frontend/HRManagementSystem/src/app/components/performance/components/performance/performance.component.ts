import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../../../common/shared/shared/shared.module';
import { PerformanceModel } from '../../models/performance.model';
import { Department } from '../../../employee/models/departmant.enum';
import { PerformanceService } from '../../services/performance.service';

@Component({
  selector: 'app-performance',
  imports: [SharedModule],
  templateUrl: './performance.component.html',
  styleUrl: './performance.component.css'
})
export class PerformanceComponent  implements OnInit{
performances: PerformanceModel[] = [];
department = Department;
userId: string;

constructor(
  private _performance: PerformanceService
){
this.userId = localStorage.getItem('userId');
}
  ngOnInit(): void {
    this.getPerformances()
  }

  getPerformances(){
    this._performance.getPerformanceByEmployeeId(this.userId,res=>{
      this.performances = res.data;
    });
  }

// Performans puanlarını yıldız sayısına çeviren fonksiyon
getStars(score: number): number[] {
  const fullStars = Math.round(score / 2); // Puanı 10 üzerinden hesaplıyoruz
  return new Array(fullStars).fill(0); // Yıldız sayısını döndürüyoruz
}

getPerformanceRating(score: number): string {
  if (score >= 8) {
    return 'status-good';
  } else if (score >= 5) {
    return 'status-medium';
  } else {
    return 'status-bad';
  }
}

}
