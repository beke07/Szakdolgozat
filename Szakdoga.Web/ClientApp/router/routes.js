import HomePage from 'components/user/home-page'
import AdminPage from 'components/admin/admin'
import Users from 'components/admin/users/users'
import Login from 'components/login'
import Projects from 'components/admin/projects/projects'
import Hierachy from 'components/admin/hierarchy/hierarchy'
import UserDetails from 'components/admin/users/user-details'
import NewUser from 'components/admin/users/new-user'
import Password from 'components/password'
import Ranks from 'components/admin/ranks/ranks'
import NewRank from 'components/admin/ranks/new-rank'
import NewProject from 'components/admin/projects/new-project'
import ProjectDetails from 'components/admin/projects/project-details'
import AdminUserLogin from 'components/admin-user-login'
import UserProjects from 'components/user/projects/projects'
import UserProjectDetails from 'components/user/projects/user-project-details'
import Activities from 'components/admin/activities/activities'
import NewActivity from 'components/admin/activities/new-activity'
import UserProfil from 'components/user/profil'
import ProjectTasks from 'components/user/tasks/project-tasks'
import TaskDetails from 'components/user/tasks/task-details'
import NewTask from 'components/user/tasks/new-task'
import UserTasks from 'components/user/tasks/user-tasks'

export const routes = [
  {
    name: 'admin',
    path: '/admin',
    component: AdminPage,
    display: 'Admin',
    icon: 'home',
    meta: { navRender: false, requiresAuth: true, adminAuth: true, userAuth: false }
  },
  {
    name: 'users',
    path: '/admin/user',
    component: Users,
    display: 'Munkatársak',
    icon: 'user',
    props: true,
    meta: { navRender: true, requiresAuth: true, adminAuth: true, userAuth: false }
  },
  {
    name: 'admin-user-login',
    path: '/admin/admin-user-login',
    component: AdminUserLogin,
    props: true,
    meta: { navRender: false, requiresAuth: true, adminAuth: true, userAuth: false }
  },
  {
    name: 'new-user',
    path: '/admin/new-user',
    component: NewUser,
    display: 'Új felhasználó',
    icon: 'user',
    meta: { navRender: false, requiresAuth: true, adminAuth: true, userAuth: false }
  },
  {
    name: 'user-details',
    path: '/admin/user-details/:id',
    component: UserDetails,
    props: true,
    meta: { navRender: false, requiresAuth: true, adminAuth: true, userAuth: false }
  },
  {
    name: 'projects',
    path: '/admin/projects',
    component: Projects,
    display: 'Projektek',
    icon: 'code-branch',
    props: true,
    meta: { navRender: true, requiresAuth: true, adminAuth: true, userAuth: false }
  },
  {
    name: 'project-details',
    path: '/admin/project-details/:id',
    component: ProjectDetails,
    props: true,
    meta: { navRender: false, requiresAuth: true, adminAuth: true, userAuth: false }
  },
  {
    name: 'new-projects',
    path: '/admin/new-project',
    component: NewProject,
    display: 'Új projekt',
    meta: { navRender: false, requiresAuth: true, adminAuth: true, userAuth: false }
  },
  {
    name: 'hierarchy',
    path: '/hierarchy',
    component: Hierachy,
    display: 'Hierarchia',
    icon: 'sitemap',
    props: true,
    meta: { navRender: true, requiresAuth: true, adminAuth: false, userAuth: false }
  },
  {
    name: 'login',
    path: '/login',
    component: Login,
    display: 'Login',
    icon: 'home',
    meta: { navRender: false, requiresAuth: false, adminAuth: false, userAuth: false }
  },
  {
    name: 'password',
    path: '/password',
    component: Password,
    icon: 'home',
    display: 'Jelszó változtatás',
    meta: { navRender: false, requiresAuth: false, adminAuth: false, userAuth: false }
  },
  {
    name: 'ranks',
    path: '/admin/ranks',
    component: Ranks,
    display: 'Beosztások',
    icon: 'address-card',
    meta: { navRender: true, requiresAuth: true, adminAuth: true, userAuth: false }
  },
  {
    name: 'new-rank',
    path: '/admin/ranks/new-rank',
    component: NewRank,
    display: 'Új beosztás',
    meta: { navRender: false, requiresAuth: true, adminAuth: true, userAuth: false }
  },
  {
    name: 'activities',
    path: '/admin/activities',
    component: Activities,
    display: 'Tevékenységek',
    icon: 'gavel',
    meta: { navRender: true, requiresAuth: true, adminAuth: true, userAuth: false }
  },
  {
    name: 'new-activity',
    path: '/admin/new-activity',
    component: NewActivity,
    meta: { navRender: false, requiresAuth: true, adminAuth: true, userAuth: false }
  },
  {
    name: 'home',
    path: '/home',
    component: HomePage,
    display: 'Home',
    icon: 'home',
    meta: { navRender: false, requiresAuth: true, adminAuth: false, userAuth: true }
  },
  {
    name: 'user-projects',
    path: '/user/projects',
    component: UserProjects,
    display: 'Projektjeim',
    icon: 'code-branch',
    meta: { navRender: true, requiresAuth: true, adminAuth: false, userAuth: true }
  },
  {
    name: 'user-project-details',
    path: '/user/project-details/:id',
    component: UserProjectDetails,
    meta: { navRender: false, requiresAuth: true, adminAuth: false, userAuth: true }
  },
  {
    name: 'user-tasks',
    path: '/user/user-tasks',
    component: UserTasks,
    display: 'Taszkjaim',
    icon: 'tasks',
    meta: { navRender: true, requiresAuth: true, adminAuth: false, userAuth: true }
  },
  {
    name: 'user-profil',
    path: '/user/profil',
    component: UserProfil,
    display: 'Profil',
    icon: 'user',
    meta: { navRender: true, requiresAuth: true, adminAuth: false, userAuth: true }
  },
  {
    name: 'project-tasks',
    path: '/user/project-tasks/:id',
    component: ProjectTasks,
    meta: { navRender: false, requiresAuth: true, adminAuth: false, userAuth: true }
  },
  {
    name: 'task-details',
    path: '/user/task-details/:taskId',
    component: TaskDetails,
    meta: { navRender: false, requiresAuth: true, adminAuth: false, userAuth: true }
  },
  {
    name: 'new-task',
    path: '/user/new-task',
    component: NewTask,
    meta: { navRender: false, requiresAuth: true, adminAuth: false, userAuth: true }
  }
]
