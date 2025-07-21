export interface ICourses {
  id: number;
  courseId: number;
  courseCode: string;
  courseName: string;
  groupCode: string;
  groupId: number;
  courseStatus: number;
  certificateNumber?: string;
  certificateLink?: string;
  startingDate: string;
  endDate?: string;
  pedLoad: number;
  userId: string;
}
