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
 
 > 1.订阅事件
```
EventManager.AddListener(StylusEvent.Press, OnPointPress);
EventManager.AddListener(StylusEvent.Release, OnPointRelease);
EventManager.AddListener(UIEvent.PopUp, OnTipsReceive);           
EventManager.AddListener(ScriptEvent.Amount, OnScriptMounted); 
```

> 2.分发事件
```
EventManager.Invoke(new StylusEventArgs(StylusEvent.Exit, gameObject, selected));
EventManager.Invoke(new StylusEventArgs(StylusEvent.Enter, gameObject, selected));
EventManager.Invoke(new StylusEventArgs(StylusEvent.Enter, gameObject, selected));
```

> 3.移除事件
```
EventManager.DelListener(StylusEvent.Enter, OnPointEnter); //移除时，可以指定事件类型
EventManager.DelListener(OnPointExit);//移除时也可以不指定事件类型
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

# 友情提示
事件总线是静态的，所以请养成在 OnDestory 方法中移除事件的好习惯，总线是大家的，自己屁股自己擦，不留后患给大家。
（啥子后患哦？ 就是莫名其妙的报 null 呗~）

# 我的简书：
[Unity 3D 教你打造自己的EventSystem(事件总线) - 简书](https://www.jianshu.com/p/bf82beb41f7f)



