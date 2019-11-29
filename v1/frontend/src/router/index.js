import Vue from 'vue';
import Router from 'vue-router';
import MovieDetail from '@/components/movies/MovieDetail';
import Movies from '@/components/movies/Movies';
import Home from '@/components/movies/Home';
import Login from '@/components/users/Login';
import LoginFailed from '@/components/users/LoginFailed';
import Logout from '@/components/users/Logout';
import SignUp from '@/components/users/SignUp';
import SignUpSuccess from '@/components/users/SignUpSuccess';
import Profile from '@/components/users/Profile';
import store from '../store/store';

Vue.use(Router);

const router = new Router({
  mode: 'history',
  base: '/',
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home,
    },
    {
      path: '/movies',
      name: 'movies',
      component: Movies,
      meta: { requiresAuth: false },
    },
    {
      path: '/:imdbId/movie',
      component: MovieDetail,
      name: 'movie',
      meta: { requiresAuth: true },
      props: true,
    },
    {
      path: '/login',
      name: 'login',
      component: Login,
    },
    {
      path: '/login-failed',
      name: 'loginFailed',
      component: LoginFailed,
    },
    {
      path: '/logout',
      name: 'logout',
      component: Logout,
    },
    {
      path: '/signup',
      name: 'signUp',
      component: SignUp,
    },
    {
      path: '/profile',
      name: 'profile',
      component: Profile,
    },
    {
      path: '/signupsuccess/:userName',
      name: 'signUpSuccess',
      component: SignUpSuccess,
    },
  ],
});

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (store.getters.loggedIn) {
      next();
      return;
    }
    next('/');
  } else {
    next();
  }
});

export default router;
