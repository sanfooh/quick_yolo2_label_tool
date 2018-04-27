## 背景
接触yolo时，它的配置其实很简单，只不过没有相应的整合工具，网上也有一些人做的工具，感觉还是不好用，操作起来速度不够快，于是用c#写了一个小工具，

## 相关教学视频
 [讲解视频](https://edu.csdn.net/course/detail/7620)
## 特点
> * 约定胜于配置：这个工具会把darknet所需的三个配置文件，两个索引文件（共5个文件），以及图片相应的标注文件整合到一个叫output的目录，然后我会将整个outpu目录拷出来使用。这样操作就会比较快，
> * 快速标注：当对图进行标注时，主要是通过左键在图片划取区域，右键完成标注自动跳到下一副图，如果按delete键就会剔除不要的图。这样就可以快速图片标注。
> * 支持一图多个标注对象，只要多次划取即可
> * 每个listbox都有响应delete事件，选中项按delete即可删除
> * 最终在使用时，work目录还少了一个预训练文件，可在这里[下载](https://pjreddie.com/media/files/darknet19_448.conv.23)
> * 生成三个配置需要人工去检查一下相应的目录，保证训练没有问题，如对三个配置文件有疑惑可参考[这里](https://github.com/sanfooh/yolo_truck)。
> * 已添加自动抓取图片功能

## 操作截图
![demo](https://github.com/sanfooh/quick_yolo2_label_tool/blob/master/demo.gif)
