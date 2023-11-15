# EChartsGen

在后端使用ECharts库，将ECharts图表Png图像文件导出到本地磁盘

![EChartsGen](./Assets/p1.png)

## 快速开始

在项目中引用[EChartsGen]( https://www.nuget.org/packages/EChartsGen)

```
dotnet add package EChartsGen
```

指定一个option用于生成Echarts，option是一个json对象，用于配置图表的各个属性，从而定义图表的展示形式。详情请查看官网：https://echarts.apache.org/zh/option.html

```
Exporter exporter = new Exporter();
var pngPath = await exporter.ExportAsync(new EChartsGen.ExportOption() { ChartOptionPath = "D:\\option.json" });
```


## ExportOption 说明

* ChartOptionPath - Option的json文件路径
* ChartOption - Option的.Net对象
* Height - 图片高度
* Width - 图片宽度

ChartOptionPath和ChartOption二选一，如果同时指定，优先使用ChartOptionPath。
图片的高度和宽度默认为1920*800，你可以根据实际情况调整。


## 示例

1. 运行单元测试
2. 前往[EChartsGen_DocTemplateTool_Sample](./EChartsGen_DocTemplateTool_Sample/)，查看使用文档模板生成工具将ECharts图形填充到Word文档



## 已知问题



## 作者信息

作者：林小

邮箱：jevonsflash@qq.com



## License

The MIT License (MIT)
