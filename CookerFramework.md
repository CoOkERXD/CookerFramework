# CookerFramework

## api层的配置

### 类库引用

只需要为你的api层引用 `Cooker.Api` ，`Cooker.Core.MySql` 这两个类库即可

## program.cs的配置

```csharp
using UserSystem.Core;

var builder = WebApplication.CreateBuilder(args);

builder.InitApplication();
builder.Services.AddService("YourPrijectName");
builder.Services.AddCookerDbContext<YourDbContext>(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddlewares();

app.Run();
```

## 基本功能介绍

### Application层

在您的项目中，api层需要引用所有的项目，因为在 `CookerFramework` 的容器注入中需要通过反射获取被 `IocTag` 标记的类和接口，通过项目引用关联才能正确找到类库

### Core层

自己的 `dbcontext` 需继承 `CookerDbContext`，因为 `ef` 对于 `MySql` 的乐观锁没有自增，所以 暂时需要自己手动++，仓储提供了 `IQueryable` 的接口可以提取使用

### Common层

`common`  层分为两个部分，一个是 `Redis` 缓存一个是 `Jwt` 模块

### Redis缓存

从命名 `Cooker.Cache.Redis` 就能看出他只提供简单的缓存 `string` 的功能，我封装了一个 `RedisHelper` 来帮助业务的实现，用法也很简单

```csharp

```

#### Jwt模块

通过new一个tokenmodel并给jwthelper输入claim即可
