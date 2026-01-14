import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Course } from '../models/course.model';
import { CourseService } from '../services/course.service';
import { CartService } from '../services/cart.service';

@Component({
  selector: 'app-courses-list',
  imports: [CommonModule, FormsModule],
  templateUrl: './courses-list.html',
  styleUrl: './courses-list.css'
})
export class CoursesList implements OnInit {
  title: string = 'Available courses';
  courses: Course[] = [];
  loading: boolean = false;
  error: string = '';
  success: string = '';
  showForm: boolean = false;
  editingCourse: Course | null = null;

  // Form model
  courseForm: Course = this.getEmptyCourse();

  constructor(
    private courseService: CourseService,
    private cartService: CartService
  ) { }

  ngOnInit(): void {
    this.loadCourses();
  }

  loadCourses(): void {
    this.loading = true;
    this.error = '';
    this.courseService.getCourses().subscribe({
      next: (data: Course[]) => {
        this.courses = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching courses', err);
        this.error = 'Failed to load courses';
        this.loading = false;
      }
    });
  }

  getEmptyCourse(): Course {
    return {
      id: '',
      title: '',
      description: '',
      price: 0,
      numberOfLessons: 1
    };
  }

  openCreateForm(): void {
    this.editingCourse = null;
    this.courseForm = this.getEmptyCourse();
    this.showForm = true;
  }

  openEditForm(course: Course): void {
    this.editingCourse = course;
    this.courseForm = { ...course };
    this.showForm = true;
  }

  closeForm(): void {
    this.showForm = false;
    this.editingCourse = null;
    this.courseForm = this.getEmptyCourse();
  }

  saveCourse(): void {
    if (this.editingCourse) {
      // Update existing course
      this.courseService.updateCourse(this.courseForm).subscribe({
        next: () => {
          this.loadCourses();
          this.closeForm();
        },
        error: (err) => {
          console.error('Error updating course', err);
          this.error = 'Failed to update course';
        }
      });
    } else {
      // Create new course
      this.courseService.addCourse(this.courseForm).subscribe({
        next: () => {
          this.loadCourses();
          this.closeForm();
        },
        error: (err) => {
          console.error('Error creating course', err);
          this.error = 'Failed to create course';
        }
      });
    }
  }

  deleteCourse(id: string): void {
    if (confirm('Are you sure you want to delete this course?')) {
      this.courseService.deleteCourse(id).subscribe({
        next: () => {
          this.loadCourses();
        },
        error: (err) => {
          console.error('Error deleting course', err);
          this.error = 'Failed to delete course';
        }
      });
    }
  }

  addToCart(courseId: string): void {
    this.cartService.addToCart(courseId).subscribe({
      next: () => {
        this.success = 'Course added to cart!';
        setTimeout(() => this.success = '', 3000);
      },
      error: (err) => {
        console.error('Error adding to cart', err);
        this.error = 'Failed to add course to cart';
        setTimeout(() => this.error = '', 3000);
      }
    });
  }
}

