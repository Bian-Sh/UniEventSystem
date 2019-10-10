# UniEventSystem
# 通用事件总线 For Unity

# 简述
* 事件总线虽然是全局的,但这个事件系统使用了 Enum 对象来装载事件的枚举值,保证了事件链中 key 值的 唯一性、多样性、高辨识度和可扩展性（枚举的优势）。
* 相较于常见框架中以 int 值做为事件链的 key值的做法，避免了必须为所有枚举指定一个独一无二的 int值 的操作,这个操作似乎不利于可持续发展？
* 这个事件系统 API 风格回归Unity，使用起来更加亲切：

|API|Description|
|---|------|
AddListener(xx) | 订阅事件
DelListener(xx) | 移除事件
RemoveAllListener() | 移除所有事件
Invoke(xx) |分发事件

 # 使用方法：
 
 > 1.订阅事件 （演示了监听不同枚举描述的不同事件种类）
```
EventManager.AddListener(StylusEvent.Press, OnPointPress);
EventManager.AddListener(StylusEvent.Release, OnPointRelease);

EventManager.AddListener(ScriptEvent.Amount, OnScriptMounted); 
EventManager.AddListener(ColorEvent.ChangeTo, OnColorChangeRequired);
EventManager.AddListener(UIEvent.PopUp, OnTipsReceive);       
```

> 2.分发事件（链式风格）
```
  EventManager.Allocate<StylusEventArgs>() //从Pool 里面分配参数类型实例
    .Config( StylusEvent.Press,gameObject,target.gameObject) //配置参数类型实例
    .Invoke(); //分发事件
                
  // 演示订阅其他类型的事件
  EventManager.Allocate<ColorEventArgs>()
    .Config(ColorEvent.ChangeTo, gameObject, c)
    .Invoke();

```

> 3.移除事件
```
EventManager.DelListener(StylusEvent.Enter, OnPointEnter); //移除时，可以指定事件类型
EventManager.DelListener(OnPointExit);//移除时也可以不指定事件类型
EventManager.DelListener(StylusEvent.Enter);//移除时也可以通过指定 事件种类 来移除这一类的所有事件
EventManager.RemoveAllListener(); //可以使用该方法全部移除
```

> 4. 处理事件：

继承 BaseEventArgs 实现自定义事件信息类，可以在事件中传递丰富的信息,但请进行必要的里氏转换哈。
```
 private void OnScriptMounted(BaseEventArgs obj)
 {
     ScriptEventArgs args = obj as ScriptEventArgs;
     //do something 
 }

 private void OnTipsReceive(BaseEventArgs obj)
 {
     UIEventArgs args = obj as UIEventArgs;
     //do something 
 }
 private void OnPointPress(BaseEventArgs obj)
 {
     StylusEventArgs args = obj as StylusEventArgs;
     //do something 
 }

```
# 动画演示
![](https://upload-images.jianshu.io/upload_images/3600713-b2ae5147fd58b5dd.gif?imageMogr2/auto-orient/strip)

# 友情提示
* 事件总线是静态的，所以请养成在 OnDestory 方法中移除事件的好习惯，总线是大家的，自己屁股自己擦，不留后患给大家。
（啥子后患哦？ 就是莫名其妙的报 null 呗~）
* 这些事件都是 Oneshot 事件，就是说事件传递的参数中的各种引用会在执行完毕后被回收。

# 关于拆箱装箱：
虽然这个事件总线的设计思路虽然很棒，但前提是装箱不耗性能的情况下。

实际上，虽然在事件总线内部逻辑中未发现装箱拆箱操作，但是在配置参数类和订阅事件的时候会各进行一次装箱操作。

如下图：

![Config函数使用时的装箱](doc/01.png)

![Config函数使用时的装箱](doc/02.png)

**但是也不用那么担心：**
* 首先，事件的订阅是可枚举出来的，也就是说不会产生那么大的数量级的装箱。

* 其次，事件的执行也是条件触发式的，而不是说每时每刻都在装箱。

* 再次，现在的设备性能，运算水平，这点毛毛雨的性能损耗也上不了台面，反而更应该确认在使用类似 Debug.LogFormat 这样的API的GC问题。

* 最后，相对于即得的优惠，和明面上的装箱性能损耗，我还是挺心安的，仅供参考。

**解决方案**
短期还是准备将每一个 枚举指导一个独一无二的 int值
为了便利起见，这个值应该由编辑器工具自动生成
当然，args 类也能自动生成一个模板也是极好的，后续只需要在模板的基础上增加关心的字段即可

只希望吧，这个装箱损耗能被解决掉就好咯~~

# 我的简书：
[Unity 3D 教你打造自己的EventSystem(事件总线) - 简书](https://www.jianshu.com/p/bf82beb41f7f)



