import { Component, Input, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SDGScore } from '../../models/project.models';

@Component({
  selector: 'app-sdg-chart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sdg-chart.component.html',
  styleUrl: './sdg-chart.component.scss',
})
export class SdgChartComponent {
  @Input() sdgScores: SDGScore[] = [];
  @Input() projectTitle: string = '';

  getAverageScore(): number {
    if (this.sdgScores.length === 0) return 0;
    const total = this.sdgScores.reduce((sum, score) => sum + score.score, 0);
    return Math.round(total / this.sdgScores.length);
  }

  getScoreColor(score: number): string {
    if (score >= 80) return '#10b981'; // Green
    if (score >= 60) return '#f59e0b'; // Yellow
    if (score >= 40) return '#f97316'; // Orange
    return '#ef4444'; // Red
  }

  getScoreLabel(score: number): string {
    if (score >= 80) return 'Excellent';
    if (score >= 60) return 'Good';
    if (score >= 40) return 'Fair';
    return 'Needs Improvement';
  }
}
