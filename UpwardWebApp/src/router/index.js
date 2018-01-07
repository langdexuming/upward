import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import Car from '@/components/Car'
import Tool from '@/components/Tool'
import Guide from '@/components/Guide'
import Home from '@/components/Home'

Vue.use(Router)

Vue.directive('title', {
  inserted: function (el, binding) {
    document.title = el.dataset.title
  }
});

export default new Router({
  routes: [{
      path: '/guide',
      component: Guide,
      name: '',
      hidden: true
    },
    {
      path: '/tool',
      component: Tool,
      name: '',
      hidden: true
    },
    {
      path: '/helloworld',
      component: HelloWorld,
      name: '',
      hidden: true
    },
    {
      path: '/car',
      component: Car,
      name: '',
      hidden: true
    },
    {
      path: '/',
      component: Home,
      name: 'home',
      children: [
        // {
        //   path: '/table',
        //   component: Table,
        //   name: 'Table'
        // },
        // {
        //   path: '/form',
        //   component: Form,
        //   name: 'Form'
        // },
        // {
        //   path: '/user',
        //   component: user,
        //   name: '列表'
        // },
      ]
    }
  ]
})
