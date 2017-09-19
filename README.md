# Genji概览

<img src="https://oss.aiursoft.com/MyPersonalFiles/genji.jpeg" alt="Drawing" style="width: 200px;"/>

源氏，英文名为Genji，是一款强大、开源、高性能、跨平台、现代化、MVVM的Web应用开发框架。  

Genji由C#开发，并基于 .NET Standard，使它可以面向任意的.NET平台，例如 .NET Core，并兼容不同的操作系统。

Genji作为HTTP服务器全部底层代码都是基于TCP/IP实现。它存在的意义是代替 ASP.NET Core。


## Genji社区

目前尚无可以讨论Genji开发的在线社区。我们正在积极的开发和准备。

## Genji的优秀特性

* 极高的启动速度  
Genji的启动速度比ASP.NET快至少30倍，不足1秒即可完全加载并达到最大性能。Genji视图更支持JIT编译，后端业务也可以像静态文件一样快速响应。

* 简洁清爽的设计风格  
Genji使用自动路由、依赖注入、MVVM设计模式、完整异步支持、强类型，让你的前端代码与后端代码保持分离，又具有最安全而便捷的交互。

* 超多的自定义选项  
Genji在保持简洁的基础下，可以像积木一样轻松插入和更换模块。注册自由定义的服务、应用绑定、中间件、过滤器或扩展Genji都极其简便。

* 支持强大的C#
Genji在设计时支持开发者使用C#进行开发。C#不但支持语言级异步、函数化编程、强类型、面向对象等众多出色的特性，更具有接近机器代码的性能。

* 极低的资源占用  
Genji在保证性能的前提下，花费了大量精力优化线程、垃圾的处理，使Genji的内存一般不超过20Mb。凭借语言级异步的支持，Genji不会阻塞IO。

* 优秀繁多的工具支持  
使用Microsoft强大的Visual Studio或是跨平台的Visual Studio Code都可以享受到完整的开发Genji时自动完成和实时跟踪调试功能。

* 支持测试  
使用 .NET Core的Xunit可以轻松为Genji创建单元测试和集成测试，并且使用Visual Studio的测试工具或流行的CI来跟踪Genji。

* 跨越操作系统和开发框架  
Genji不但可以运行与Windows、Linux、Mac上，凭借其跨开发框架的特性，无论你是 .NET Framework开发者还是Xamarin开发者，都可以在你自己面向的平台中访问全部特性。

## 运行项目示例

### Windows

在Windows平台，强烈推荐使用[Visual Studio 2017](https://www.visualstudio.com/)开发Genji。

打开`Genji.sln`后，按下`F5`启动项目即可。



### Windows, Linux or macOS

你可以使用CLI工具来运行，手动安装下面组件:  
* [.NET Core runtime](https://www.microsoft.com/net)
* [Nodejs](https://nodejs.org/en/) & [npm](https://www.npmjs.com/)
* [Bower](https://bower.io/) & [Git](https://git-scm.com/)
* [Visual Studio Code](https://code.visualstudio.com/) （推荐）

在仓库的根目录执行下面命令：  

```bash
cd GenjiExample/  
dotnet restore  
bower install  

dotnet run
```

## 安装Genji作为依赖项

TODO

## 详细的Genji文档

* 简介
* 快速入门
    * 使用Genji开发音乐商店
* 基础知识
    * 应用程序启动
    * Genji服务器结构
    * 应用绑定
    * 日志服务
* 中间件
    * 静态文件中间件
    * 默认文件中间件
    * 404中间件
* MVVM
    * 插入MVC中间件
    * 控制器
    * 依赖注入
    * 业务
    * 修饰器
    * 模型
    * 视图模型
    * 构造API
* 视图语法
    * gj-model
    * gj-value
    * gj-if
    * gj-for
    * gj-layout
    * gj-render
* 测试和调试
* 连接到数据库
* 客户端编程
* 部署
* 安全性
* 性能


## 如何为Genji贡献