import Vue from 'vue'
import Router from 'vue-router'

import HelloWorld from '@/components/HelloWorld'
import Car from '@/components/Car'

import Tool from '@/views/Tool/Index'
import RegExTool from '@/views/Tool/RegEx'
import LetterShiftTool from '@/views/Tool/LetterShift'
import WebSocketClient from '@/views/Tool/WebSocketClient'

import Home from '@/views/Home'
import PlatformAccountManage from '@/views/PlatformAccountManage/Index'
import AddPlatformAccount from '@/views/PlatformAccountManage/Add'


import CarToolSample from '@/views/Samples/CarTool'
import IOV from '@/views/IOV/Index'
import ResourceManage from '@/views/ResourceManage/Index'

import BlockChain from '@/views/BlockChain/Index'

Vue.use(Router)

Vue.directive('title', {
    inserted: function(el, binding) {
        document.title = el.dataset.title
    }
});

export default new Router({
    routes: [{
            path: '/Samples/CarTool',
            component: CarToolSample,
            name: '',
            hidden: true,
        },
        {
            path: '/BlockChain',
            component: BlockChain,
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
            }],
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
                },
                {
                    path: '/tool/WebSocketClient',
                    component: WebSocketClient,
                    name: 'WebSocketClient'
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
            path: '/iov',
            component: IOV,
            name: '',
            hidden: true
        },
        {
            path: '/ResourceManage',
            component: ResourceManage,
            name: '',
            hidden: true
        },
        {
            path: '/',
            component: Home,
            name: 'home',
            children: []
        }
    ]
})