import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import Car from '@/components/Car'
import Tool from '@/views/Tool/Index'
import RegExTool from '@/views/Tool/RegEx'
import LetterShiftTool from '@/views/Tool/LetterShift'
import Guide from '@/components/Guide'
import Home from '@/views/Home'
import PlatformAccountManage from '@/views/PlatformAccountManage/Index'
import AddPlatformAccount from '@/views/PlatformAccountManage/Add'

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
      path: '/PlatformAccountManage',
      component: PlatformAccountManage,
      name: '',
      hidden: true,
      children: [{
          path: '/PlatformAccountManage/Add',
          component: AddPlatformAccount,
          name: 'AddPlatformAccount'
        }
        // {
        //   path: '/PlatformAccountManage/LetterShift',
        //   component: LetterShiftTool,
        //   name: 'LetterShiftTool'
        // }
      ],
    },
    {
      path: '/tool',
      component: Tool,
      name: '',
      hidden: true,
      children: [{
          path: '/tool/RegEx',
          component: RegExTool,
          name: 'RegExTool'
        },
        {
          path: '/tool/LetterShift',
          component: LetterShiftTool,
          name: 'LetterShiftTool'
        }
      ],
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
