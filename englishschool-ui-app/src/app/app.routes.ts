import { Routes } from '@angular/router';
import { CoursesList } from './courses-list/courses-list';
import { LessonsList } from './lessons-list/lessons-list';
import { HomeworksList } from './homeworks-list/homeworks-list';
import { AssignmentsList } from './assignments-list/assignments-list';
import { CalendarList } from './calendar-list/calendar-list';
import { Cart } from './cart/cart';
import { Orders } from './orders/orders';

export const routes: Routes = [
  { path: '', redirectTo: '/courses', pathMatch: 'full' },
  { path: 'courses', component: CoursesList },
  { path: 'lessons', component: LessonsList },
  { path: 'homeworks', component: HomeworksList },
  { path: 'assignments', component: AssignmentsList },
  { path: 'calendar', component: CalendarList },
  { path: 'cart', component: Cart },
  { path: 'orders', component: Orders }
];

