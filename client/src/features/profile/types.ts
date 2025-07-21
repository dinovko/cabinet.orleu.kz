import type { ICourses } from "../courses/types";

export interface IUserProfile {
  iin: string;
  surname: string;
  firstName: string;
  middleName: string;
  nationalityId: string;
  genderId: string;
  totalEx: number;
  pedExper: number;
  birthDate: string;
  email: string;
  phone: string;
  isEmployee: boolean;
  employeeInform: null|EmployeeInformDto;
  listenerInform: null|ListenerJobDto;
  id: string;
  userName: string;
  normalizedUserName: string;
  normalizedEmail: null;
  emailConfirmed: boolean;
  passwordHash: null;
  securityStamp: string;
  concurrencyStamp: string;
  phoneNumber: string;
  phoneNumberConfirmed: boolean;
  twoFactorEnabled: boolean;
  lockoutEnd: null;
  lockoutEnabled: boolean;
  accessFailedCount: number;
  courses:ICourses[]|null;
}

export interface EmployeeInformDto {
  empId: number;
  empOrganizationId: number;
  empOrganization: string;
  employeeDepartmentId: number;
  employeeDepartment: string;
  empPositionId: number;
  empPosition: string;
}

export interface ListenerJobDto {
  listenerJobId: number;              // Идентификатор должности слушателя
  areaCode: string;                   // Код области
  regionCode: string;                 // Название региона
  schoolId: number;                   // Идентификатор школы
  school: string;                     // Название школы
  schoolBIN: string;                  // БИН школы
  pedScienceDegreeId: string;         // Ученая степень
  pedQualCategoryId: string;          // Квалификационная категория
  pedSubjectId: string;               // Преподаваемый предмет
  pedPositionId: string;              // Должность
  pedEducationTypeId: string;         // Уровень образования
}
