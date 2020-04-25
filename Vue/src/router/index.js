import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: () =>
      import(/* webpackChunkName: "home" */ "../views/home/home.vue")
  },
  {
    path: "/uploadProtectedFile",
    name: "Upload",
    component: () =>
      import(/* webpackChunkName: "home" */ "../views/upload/upload.vue")
  },
];

const router = new VueRouter({
  mode: "history",
  linkExactActiveClass: 'active',
  routes
});

export default router;
