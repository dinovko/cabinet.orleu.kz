// src/app/router.tsx
import { createBrowserRouter, Navigate, RouterProvider } from 'react-router-dom';
import MainLayout from '@/layouts/MainLayout';
import CoursesPage from '@/features/courses/pages/CoursesPage';
import ProfilePage from '@/features/profile/pages/ProfilePage';
import GroupsPage from '@/features/groups/pages/GroupsPage';
// import AuthPage from '@/features/auth/pages/AuthPage';
// import HomePage from '@/features/home/pages/HomePage';

export const router = createBrowserRouter([
  {
    path: '/',
    element: <MainLayout />,
    children: [
      { path: '', element: <Navigate to="profile" replace /> },
      { path: 'profile', element: <ProfilePage /> },
      { path: 'courses', element: <CoursesPage /> },
      { path: 'groups', element: <GroupsPage /> },
    ],
  },
]);

export const AppRouter = () => <RouterProvider router={router} />
